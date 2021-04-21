# Text File Analyzer
-----------
This simple WPF App was created as my semestral project of C#/.NET college course. It allows user to select folder containing text files and then it displays statistical information like number of words/sentences, longest/shortest word/sentence, most occured word and so on (both in folder and each file separately).

My project uses MVVM design pattern which separates UI (View) from business logic behind the app. GUI is styled and resources haven't been duplicated. There are no hard coded strings, all of them are in Resources.resx, so the program can be easily localizated. It has already been localized to English and Czech (Klingon localization is in progress). In future I plan on adding unit tests for business logic and UI.

<img src="https://i.ibb.co/wyvNqQC/textfileanalyzer.png" alt="App window">
