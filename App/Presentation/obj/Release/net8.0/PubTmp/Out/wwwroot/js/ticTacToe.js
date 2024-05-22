let board = ['', '', '', '', '', '', '', '', ''];
let currentPlayer = 'X';
let gameOver = false;
let gameMode = '';

const startGame = (mode) => {
    gameMode = mode;
    resetBoard();
    document.getElementById('board').removeAttribute('hidden');
};

const resetBoard = () => {
    board = Array(9).fill('');
    document.querySelectorAll('.cell').forEach(cell => cell.innerText = '');
    document.querySelector('.message').innerText = '';
    document.querySelector('.messageuk').innerText = '';
    gameOver = false;
    currentPlayer = 'X';
};

const makeMove = (index) => {
    if (gameOver || board[index]) return;
    board[index] = currentPlayer;
    document.getElementById('board').children[index].innerText = currentPlayer;

    if (checkWinner()) {
        document.querySelector('.message').innerText = `${currentPlayer} wins!`;
        document.querySelector('.messageuk').innerText = `${currentPlayer} виграв(-ла)!`;
        gameOver = true;
        return;
    }

    if (board.every(cell => cell)) {
        document.querySelector('.message').innerText = 'It\'s a draw!';
        document.querySelector('.messageuk').innerText = 'Це нічия!';
        gameOver = true;
        return;
    }

    currentPlayer = currentPlayer === 'X' ? 'O' : 'X';

    if (currentPlayer === 'O' && gameMode === 'bot') computerMove();
};

const computerMove = () => {
    let bestScore = -Infinity;
    let move;
    board.forEach((cell, i) => {
        if (!cell) {
            board[i] = 'O';
            let score = minimax(board, 0, false);
            board[i] = '';
            if (score > bestScore) {
                bestScore = score;
                move = i;
            }
        }
    });
    makeMove(move);
};

const minimax = (board, depth, isMaximizing) => {
    if (checkWinner() && isMaximizing) return -10 + depth;
    if (checkWinner() && !isMaximizing) return 10 - depth;
    if (!board.includes('')) return 0;

    let bestScore = isMaximizing ? -Infinity : Infinity;
    board.forEach((cell, i) => {
        if (!cell) {
            board[i] = isMaximizing ? 'O' : 'X';
            let score = minimax(board, depth + 1, !isMaximizing);
            board[i] = '';
            bestScore = isMaximizing ? Math.max(score, bestScore) : Math.min(score, bestScore);
        }
    });
    return bestScore;
};

const checkWinner = () => {
    const winningCombos = [
        [0, 1, 2], [3, 4, 5], [6, 7, 8], // Rows
        [0, 3, 6], [1, 4, 7], [2, 5, 8], // Columns
        [0, 4, 8], [2, 4, 6]             // Diagonals
    ];
    return winningCombos.some(combo => {
        return board[combo[0]] && board[combo[0]] === board[combo[1]] && board[combo[1]] === board[combo[2]];
    });
};