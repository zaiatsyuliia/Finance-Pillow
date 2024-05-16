let secretNumber;
let attemptsLeft = 10;

function startBnC() {
    document.getElementById('winningMessage').style.display = 'none';
    attemptsLeft = 10;
    const numberLength = document.getElementById('numberLength').value;
    if (numberLength < 4 || numberLength > 10) {
        showAlert("Please choose a number length between 4 and 10.");
        return;
    }

    document.getElementById('userGuess').maxLength = numberLength;
    document.getElementById('userGuess').value = '';

    secretNumber = generateNumber(numberLength);
    document.getElementById('gameArea').style.display = 'block';
    document.getElementById('attempts').innerText = attemptsLeft;
    document.getElementById('resultsList').innerHTML = '';
}

function startBnCuk() {
    document.getElementById('winningMessage').style.display = 'none';
    attemptsLeft = 10;
    const numberLength = document.getElementById('numberLength').value;
    if (numberLength < 4 || numberLength > 10) {
        showAlert("Виберіть довжину числа від 4 до 10.");
        return;
    }

    document.getElementById('userGuess').maxLength = numberLength;
    document.getElementById('userGuess').value = '';

    secretNumber = generateNumber(numberLength);
    document.getElementById('gameArea').style.display = 'block';
    document.getElementById('attempts').innerText = attemptsLeft;
    document.getElementById('resultsList').innerHTML = '';
}

function generateNumber(length) {
    let number = '';
    while (number.length < length) {
        const digit = Math.floor(Math.random() * 10);
        if (!number.includes(digit)) {
            number += digit;
        }
    }
    return number;
}

function checkGuess() {
    document.getElementById('winningMessage').style.display = 'none';
    const userGuess = document.getElementById('userGuess').value;

    if (new Set(userGuess).size !== userGuess.length) {
        showAlert("Please enter a number with unique digits.");
        return;
    }

    const numberLength = document.getElementById('numberLength').value;
    if (userGuess.length !== parseInt(numberLength, 10)) {
        showAlert(`Please enter a ${numberLength}-digit number.`);
        return;
    }

    let bulls = 0;
    let cows = 0;

    for (let i = 0; i < userGuess.length; i++) {
        if (userGuess[i] === secretNumber[i]) {
            bulls++;
        } else if (secretNumber.includes(userGuess[i])) {
            cows++;
        }
    }

    document.getElementById('userGuess').value = '';
    const resultItem = document.createElement('p');
    resultItem.textContent = `${userGuess}: B${bulls} C${cows}`;
    document.getElementById('resultsList').appendChild(resultItem);

    attemptsLeft--;

    document.getElementById('attempts').innerText = attemptsLeft;

    if (bulls === secretNumber.length) {
        showAlert(`Congratulations! You guessed the number: ${secretNumber}.`);
        startGame();
    }

    if (attemptsLeft === 0) {
        showAlert(`Game over! The secret number was ${secretNumber}. 
        You stopped at ${userGuess}: B${bulls} C${cows}.`);
        startGame();
    }
}

function checkGuessuk() {
    document.getElementById('winningMessage').style.display = 'none';
    const userGuess = document.getElementById('userGuess').value;

    if (new Set(userGuess).size !== userGuess.length) {
        showAlert("Будь ласка, введіть номер з унікальними цифрами.");
        return;
    }

    const numberLength = document.getElementById('numberLength').value;
    if (userGuess.length !== parseInt(numberLength, 10)) {
        showAlert(`Будь ласка, введіть число з довжиною ${numberLength}.`);
        return;
    }

    let bulls = 0;
    let cows = 0;

    for (let i = 0; i < userGuess.length; i++) {
        if (userGuess[i] === secretNumber[i]) {
            bulls++;
        } else if (secretNumber.includes(userGuess[i])) {
            cows++;
        }
    }

    document.getElementById('userGuess').value = '';
    const resultItem = document.createElement('p');
    resultItem.textContent = `${userGuess}: Б${bulls} К${cows}`;
    document.getElementById('resultsList').appendChild(resultItem);

    attemptsLeft--;

    document.getElementById('attempts').innerText = attemptsLeft;

    if (bulls === secretNumber.length) {
        showAlert(`Вітаю! Ви вгадали число: ${secretNumber}.`);
        startGame();
    }

    if (attemptsLeft === 0) {
        showAlert(`Гра завершена! Номер був ${secretNumber}.
        Ви зупинились на ${userGuess}: B${bulls} C${cows}.`);
        startGame();
    }
}

function showAlert(message) {
    const winningMessageText = document.getElementById('winningMessageText');
    winningMessageText.textContent = message;
    document.getElementById('winningMessage').style.display = 'block';
}