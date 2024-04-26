const originalSongs = [
    'A Melody to Break the Curse.mp3',
    'Alls Well',
    'Aria Math',
    'Blood and Wine',
    'Civilisations Lost',
    'Crisis City',
    'Dad Battle',
    'Dearest Hue',
    'Farewell',
    'Final Fortress',
    'Karasu',
    'Kingdom Valley',
    'Kronos Island',
    'Living Mice',
    'M.I.L.F',
    'Out of time',
    'Phantoms Legacy',
    'Restoring the Light, Facing the Dark',
    'Resurrections',
    'Sand Ruins',
    'Sealed Ground',
    'Seaside Hill',
    'Splash Canyon',
    'Sweden',
    'Through the Valleys',
    'Un-gravitify',
    'Walking The Walk',
    'Windy and Ripply',
    'Windy Hill'
];

let songs = [];

function shuffleArray(array) {
    for (let i = array.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [array[i], array[j]] = [array[j], array[i]];
    }
    return array;
}

function initializeSongs() {
    songs = shuffleArray([...originalSongs]); 
}

function audioStart() {
    initializeSongs();

    const song = songs.pop();
    audioPlayer.src = "/music/" + song + ".mp3";
}

function changeSong() {
    if (songs.length === 0) {
        initializeSongs();
    }

    const audioPlayer = document.getElementById('audioPlayer');
    const nextSong = songs.pop();
    audioPlayer.src = "/music/" + nextSong + ".mp3";

    audioPlayer.play();
}

document.getElementById('audioPlayer').addEventListener('ended', function () {
    changeSong();
});

var audio = document.getElementById('audioPlayer');

function togglePlayPause() {
    if (audio.paused) {
        audio.play();
    } else {
        audio.pause();
    }
}

window.onload = audioStart;