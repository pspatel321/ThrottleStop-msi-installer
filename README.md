# ThrottleStop Installer Package
This project is a convenient .msi installer around the TechPowerUp [ThrottleStop](https://www.techpowerup.com/download/techpowerup-throttlestop) utility by Kevin Glynn. It provides Windows "Add or remove programs" integration and installs into "Program Files" for an easy to maintain and remove package. Creation of the automatic startup task is also integrated into the installer. The task runs on user login after a 1-minute delay to allow user to stop/alter the process in case of bad configuration of ThrottleStop's .ini settings file.

This repo can also be used as a template to wrap other portable software programs with an .msi installer.

# Quick install
Go to the releases section and download the latest Windows installer. The ThrottleStop version number is provided in the release description. Run the .msi to install ThrottleStop. It can be uninstalled easily through "Add or Remove programs" or other similar methods.

# Building from source
The repo is configured to allow easy building of new ThrottleStop versions. If you would like to integrate your own version of the base ThrottleStop package or you want to verify authenticity, follow these instructions. It requires Visual Studio 2019 (2017/2015 may work), [Microsoft Visual Studio Installer Projects](https://marketplace.visualstudio.com/items?itemName=VisualStudioClient.MicrosoftVisualStudio2017InstallerProjects) extension, and Python3.7 or compatible.

The repo contains no binary files from the original ThrottleStop project. You must run the configure script which will interactively guide you through the process of locating the .zip package. If you want to skip the chatter, just paste a copy of the .zip package (for example, ThrottleStop_8.70.6.zip, as downloaded from ThrottleStop's link) in the solution directory beside configure.py. Run the Python script like this: "py configure.py". It will unzip your file and insert its resources into ThrottleStop.setup.csproj. "py configure.py --clean" will undo this process and remove the content.

Now open Visual Studio on the solution file. If you already had it open at the time, Reload the project. Click Build Solution and the output .msi installer will be created.  The Visual Studio solution contains both a C# project (the custom actions for startup task creation) and a Windows Setup project (the installer).  This project is kept simple and mostly standard/default settings.

# License
Fully-permissive under the [Unlicense](LICENSE).
