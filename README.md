# OWDemo
A tool for Counter-Strike: Global Offensive which allows you to find the unencrypted demo of your Overwatch case.

**Disclaimer**: *This tool was tested under Windows 7 64-Bit and is not guaranteed to work under other Windows versions.

## Table of Contents  
[1. Dependencies](#dependencies)  
[2. Installation](#installation)  
[3. Usage](#usage)  

## Dependencies
You will need to have these things installed to run the application. If you're unsure whether or not you already have one of these installed, try running the application to see if an error message comes up.  

| Name                                              | Version         | Website/Download link                                                                                                                       |
|---------------------------------------------------|-----------------|---------------------------------------------------------------------------------------------------------------------------------------------|
| WinPcap                                           | 4.1.3 or higher | http://www.winpcap.org/                                                                                                                     |
| .NET Framework                                    | 4.5.2 or higher | https://www.microsoft.com/en-us/download/details.aspx?id=42642                                                                              |
| Microsoft Visual C++ 2010 Redistributable Package | x86 or x64      | [x86](http://www.microsoft.com/en-us/download/details.aspx?id=5555) or [x64](http://www.microsoft.com/en-us/download/details.aspx?id=14632) |
| WinRAR (or similar to extract .bz2 files)         | any             | http://www.rarlab.com/                                                                                                                      |


## Installation
1. Download the [latest release](https://github.com/rakijah/OWDemo/releases/latest)  
2. Extract the archive to a folder of your choice.  

## Usage
1. Start CS:GO
2. Run `OWDemo.exe` 
3. Follow the instructions to choose your main network device
4. When `Now listening...` is shown, click on the in-game `DOWNLOAD EVIDENCE` button for your Overwatch case  
5. The download link should be printed to the output window as soon as the download starts:  
![Example output image](https://raw.githubusercontent.com/rakijah/OWDemo/master/img/output.png)  
6. Choose to copy the link to clipboard or to open it in your browser to start the download
7. Extract the demo file somewhere in your CS:GO folder (for example: `C:\Program Files (x86)\Steam\steamapps\common\Counter-Strike Global Offensive\csgo`)
8. Click `REVIEW EVIDENCE` in-game to start viewing the case.
9. Try to find an identifiable detail (unique play, k/d in a certain round etc.) for the suspect
10. Either finish viewing the demo and submit your verdict or skip to the end and press "Postpone Judgement`
10. Use `playdemo <demoname>` to play the unencrypted demo and look for the identifying criteria to find the suspect
11. Click on his name in the scoreboard to get his Steam profile