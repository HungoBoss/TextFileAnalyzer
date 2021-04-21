# Text File Analyzer
-----------
This simple WPF App was created as my semestral project in college course of C#/.NET. It allows user to select folder containing text files and then it displays statistical information like number of words/sentences, longest/shortest word/sentence, most occured word aso. (in each file and folder).

My project uses MVVM design pattern which separates UI (View) from business logic behind the app. GUI is styled and all resources are not duplicated as much as they can be. There are no hard coded strings, all of them are in Resources.resx, so the program can be easily localizated. It has already been localized to English and Czech (Klingon localization is in progress). In future I plan on adding unit tests both for business logic and UI.

<img src="https://i.ibb.co/wyvNqQC/textfileanalyzer.png" alt="App window">
