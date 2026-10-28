@echo off
rem <ドライブCの検査データをネットワークサーバにコピー>
xcopy "C:\耐電圧試験機_D(Main)" "\\10.36.14.215\fam3\耐電圧D\" /D /S /E /C /F /H /R /K /Y /Z
rem pause
