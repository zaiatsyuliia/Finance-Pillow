var words = ["HELLO", "APPLE", "COMPUTER", "TABLE", "CAR", "HOUSE", "DOG", "CAT", "PHONE", "BOOK", "CHAIR", "BANANA", "CLOUD", "SUN", "MOON", "STAR", "WATER",
    "FIRE", "EARTH", "WIND", "SNOW", "RAIN", "TREE", "FLOWER", "MOUNTAIN", "OCEAN", "RIVER", "LAKE", "ISLAND", "CITY", "VILLAGE", "ROAD", "BRIDGE", "TRAIN",
    "PLANE", "SHIP", "BICYCLE", "WATCH", "SHIRT", "PANTS", "SHOES", "GLASSES", "HAT", "GLOVES", "UMBRELLA", "JACKET", "SOCKS", "BAG"];
var selectedWord = words[Math.floor(Math.random() * words.length)];
var guessedWord = Array(selectedWord.length).fill('_');
var incorrectGuesses = 0;
var usedLetters = [];

function displayWord() {
    document.getElementById('wordToGuess').innerText = guessedWord.join(' ');
}

function checkLetter(letter) {
    if (usedLetters.includes(letter)) {
        return;
    }

    var letterFound = false;
    for (var i = 0; i < selectedWord.length; i++) {
        if (selectedWord[i] === letter) {
            guessedWord[i] = letter;
            letterFound = true;
            disableLetterButton(letter);
        }
    }

    if (!letterFound) {
        incorrectGuesses++;
        updateHangmanImage();
        disableLetterButton(letter);
    }

    usedLetters.push(letter);
    displayWord();
    checkGameStatus();
    updateAttempts();
}

function updateAttempts() {
    document.querySelector('.attempts').innerText = 'Attempts: ' + incorrectGuesses + '/6';
    document.querySelector('.attemptsuk').innerText = '??????: ' + incorrectGuesses + '/6';
}

function checkGameStatus() {
    if (guessedWord.join('') === selectedWord) {
        document.querySelector('.message').innerText = 'You won!';
        document.querySelector('.messageuk').innerText = '?? ???????!';
        disableLetterButtons();
    } else if (incorrectGuesses >= 6) {
        document.querySelector('.message').innerText = 'You lost. The word was: ' + selectedWord;
        document.querySelector('.messageuk').innerText = '?? ????????. ????? ????: ' + selectedWord;
        disableLetterButtons();
    }
}

function disableLetterButtons() {
    var letterButtons = document.querySelectorAll('.letterButton');
    letterButtons.forEach(function (button) {
        button.disabled = true;
    });
}

function disableLetterButton(letter) {
    var buttons = document.querySelectorAll('.letterButton');
    buttons.forEach(function (button) {
        if (button.innerText === letter) {
            button.disabled = true;
            button.style.backgroundColor = 'gray';
        }
    });
}

function updateHangmanImage() {
    var hangmanImage = document.getElementById('hangmanImage');
    var imagePath = `/images/hangman${incorrectGuesses}.png`;
    hangmanImage.src = imagePath;
}

function restartHangman() {
    selectedWord = words[Math.floor(Math.random() * words.length)];
    guessedWord = Array(selectedWord.length).fill('_');
    incorrectGuesses = 0;
    usedLetters = [];

    displayWord();

    var letterButtons = document.querySelectorAll('.letterButton');
    letterButtons.forEach(function (button) {
        button.disabled = false;
        button.style.backgroundColor = ''; // reset button color
    });

    document.querySelector('.message').innerText = '';
    document.querySelector('.messageuk').innerText = '';
    updateAttempts();

    var hangmanImage = document.getElementById('hangmanImage');
    hangmanImage.src = `/images/hangman0.png`;
}

displayWord();