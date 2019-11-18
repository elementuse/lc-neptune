@echo off

set filepath=%plugincodepath%\file

cd %filepath%

copy /y *.* "%plugincodepath%\..\host.winform\"

exit