OpenCover.Console.exe -register:user -target:"C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\MSTest.exe" -targetargs:"/noisolation /testcontainer:..\ChessTests\bin\Debug\ChessTests.dll" "-filter:+[Chess]* -[ChessTests]* -[Chess]*Program* -[Chess]*Properties*" -output:.\report\output.xml

ReportGenerator.exe -reports:.\report\output.xml -targetdir:.\report -reporttypes:Html,HtmlSummary

pause