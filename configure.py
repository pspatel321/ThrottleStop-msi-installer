import os, sys, argparse, zipfile, shutil, glob, xml.etree.ElementTree as xml, tempfile

# VS project file which will be modified by the script
projFile='ThrottleStop.setup\\ThrottleStop.setup.csproj'
# Name to use as search keyword for finding the .zip
programName='ThrottleStop'
# These files will be marked as "Remove" in the VS project file
ommitFiles=['ThrottleStop.exe']
# URL for user to go download file from
webUrl='https://www.techpowerup.com/download/techpowerup-throttlestop/'

if __name__=='__main__':
    parser = argparse.ArgumentParser(description='Assist in fetching package files from the web to create the Program Files directory.')
    group = parser.add_mutually_exclusive_group()
    group.add_argument('--configure', nargs='?', const='', help='User interactive procedure to retrieve the .zip file from web; optionally provide file path as the next argument')
    group.add_argument('--clean-configure', action='store_const', const=True, help='Remove web-derived files')
    args = parser.parse_args()

    # Fix paths
    runDir = os.getcwd()
    solDir = os.path.abspath(os.path.split(__file__)[0])
    os.chdir(solDir)
    projFile = os.path.abspath(projFile)
    projDir = os.path.split(projFile)[0]
    os.chdir(runDir)
    if not os.path.exists(projDir) or not os.path.exists(projFile):
        raise Exception()

    # No arguments --> default to build
    if args.configure is None and args.clean_configure is None:
        args.configure = list()

    # Convert configure (w/zip) into plain configure
    if args.configure is not None and len(args.configure) > 0:
        if not os.path.exists(args.configure):
            raise FileNotFoundError
        shutil.copy(os.path.abspath(args.configure), solDir)
        args.configure = list()
    
    # Run the rest from the project directory    
    os.chdir(projDir)

    # Find newest zip file with the program in the name
    def checkForZip(path=None):
        if path is None:
            path = os.getcwd()
        path = os.path.abspath(path)
        files = glob.glob(os.path.join(path, f'*{programName}*.zip'))
        files.sort(key=lambda x: os.path.getctime(x))
        if files is None or len(files) == 0:
            return None
        else:
            return files[-1]
    
    firstLine = ''
    # Read project file into xml etree object
    def xmlRead():
        global firstLine
        with open(projFile, 'r') as f:
            st = f.read().replace('<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">', '<Project>')
            firstLine = st[:st.find('\n') + 1]
            st = st[st.find('\n') + 1:]
            proj = xml.fromstring(st.encode())
            return proj

    # Write project file from xml etree object
    def xmlWrite(xmlObj):
        global firstLine
        st = xml.tostring(proj).decode()
        st = st.replace('<Project>', '<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">')
        st = firstLine + st
        with open(projFile, 'w') as f:
            f.write(st)
    
    # Parse xml node for .zip, "Include", "Remove" files
    def getFilesFromGroup(group:xml.Element) -> (str, list, list):
        zipf = group.get('Label')
        filesI = [e.get('Include') for e in group if e.get('Include') is not None]
        filesR = [e.get('Remove') for e in group if e.get('Remove') is not None]
        return zipf, filesI, filesR
    
    # Add .zip, "Include", "Remove" files to xml node
    def setFilesToGroup(group:xml.Element, zipf:str='', filesI:list=list(), filesR:list=list()):
        group[:] = group[0:0]
        group.set('Label', zipf)
        for f in filesI:
            e = xml.Element('Content')
            e.set('Include', f)
            group.append(e)
        for f in filesR:
            e = xml.Element('Content')
            e.set('Remove', f)
            group.append(e)

    # Clean process
    if args.clean_configure is not None:
        print('Cleaning zip program files ...')
        
        # Remove zip section from project file, noting list of filenames
        proj = xmlRead()
        group = [g for g in proj.findall('ItemGroup') if g.get('Label') is not None][-1]
        zipf, filesI, filesR = getFilesFromGroup(group)
        files = filesI + filesR
        setFilesToGroup(group)
        xmlWrite(proj)

        # Remove all files here
        if zipf is not None:
            try:
                os.remove(zipf)
            except FileNotFoundError:
                pass
        for f in files:
            if f is not None:
                try:
                    os.remove(f)
                except FileNotFoundError:
                    pass

    # Configure process
    if args.configure is not None:

        # Interactively get a new zip file
        def fetchZip():
            # Check if zip file present
            zipf = checkForZip(solDir)
            if zipf != None:
                inp = input(f'File was found containing the program files.\n"{zipf}"\nWould you like to use it for this build? [Yy/Nn] (default: Y): ')
                inp = not inp.lower().startswith('n')
                if inp:
                    return zipf
            
            # Fetch from web, interactive
            print('\n')
            print(f'You will need to fetch the .zip package of {programName} from the web.\nThis package will be extracted and utilized by the build process to create the final .msi installer.\nIt can be located through webpage: "{webUrl}"')
            inp = input(f'Paused. Press Enter to launch web browser to download the file ...')
            os.startfile(webUrl)
            
            # Wait for file to be placed and ready
            zipf = None
            while(True):
                print('\n')
                print(f'Please copy the .zip here or leave it in your user Downloads folder.')
                inp = input('Paused. Press Enter to continue ...')

                try:
                    zipf = checkForZip(solDir)
                    if zipf == None:
                        zipf = checkForZip(os.path.expanduser('~\\Downloads'))
                except:
                    pass
                if zipf is not None:
                    print(f'Valid file was found:\n"{zipf}"')
                    break
                else:
                    print('Valid .zip file not found, please try again.')
            return shutil.copy(zipf, solDir)
        
        # Checks if build is good as is, nothing more to do
        def preBuildCheck() -> bool:
            proj = xmlRead()
            group = [g for g in proj.findall('ItemGroup') if g.get('Label') is not None][-1]
            zipf, filesI, filesR = getFilesFromGroup(group)
            files = filesI + filesR

            if zipf is not None:
                if not os.path.exists(zipf):
                    return False
            for f in files:
                if f is not None:
                    if not os.path.exists(f):
                        return False
            return zipf is not None

        if preBuildCheck():
            print('Build up-to-date, nothing to do.')
            try:
                sys.exit()
            except:
                pass

        zipf = fetchZip()
        zipf = os.path.relpath(zipf, projDir)

        # Unzip here
        files = list()
        with zipfile.ZipFile(zipf, 'r') as z:
            z.extractall()
            for f in z.namelist():
                files.append(f)

        # Modify project file by adding these new items
        proj = xmlRead()
        group = [g for g in proj.findall('ItemGroup') if g.get('Label') is not None][-1]
        filesI = [f for f in files if f not in ommitFiles]
        filesR = [f for f in files if f in ommitFiles]
        setFilesToGroup(group, zipf, filesI, filesR)
        xmlWrite(proj)
        print(f'Zip program files were successfully inserted into project. Reload the VS project, or open it now to proceed with building.')