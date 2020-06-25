# XCI Explorer

[Original MaxConsole Release](https://www.maxconsole.com/threads/exclusive-xci-explorer-released-for-switch-game-cartridge-backups.47046/)

[GBATemp Release Thread](https://gbatemp.net/threads/xci-explorer-1-50mod.566877/)

View contents of XCI files and more, much more!

## Features
* View metadata for XCI and NSP files
* Explore partitions
* Check NCA hashes
* Extract NCA
* Modify cert
* Save game image
* XCI/NSP Splitter for splitting large games to fit on fat32 micro SD cards
* Show md5 of files
* Base 64 encoder & decoder
* Easily rename XCI and NSP files based on Title ID and the Orignal game Name
* Key editor (for hactool use)
* 4nxci GUI (converts XCI to NSP)
* RenXpack GUI (repacks NSP with lower firmware key)
* Database tools for links & game tracking
* Tools - SAK (for changing game formats), fat32 format tool, Cobra Crypt (for link decoding/encoding)


Main Page | Database
:-------------------------:|:-------------------------:
![Main Page](https://i.imgur.com/ZPvrS9b.png) | ![Database](https://i.imgur.com/tQL5fs8.png)

## Build Requirements
* [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
* System.Data.SQLite.Core (Nuget)
* Be.Windows.Forms.HexBox (Nuget)
* FluentFTP (Nuget)

## Build Instructions
* Open **XCI Explorer.sln**
* Install packages shown in build requirements
* Change *Debug* to *Release* in the dropdown menu
* Go to *Build*, then *Build Solution*
* Run **XCI-Explorer.exe**

* Extra tools + dll files can be found in the [Release Page](https://github.com/mrdude2478/XCI-Explorer/releases/)

## Disclaimer
Original code forked from [Student Blake](https://github.com/StudentBlake/XCI-Explorer)