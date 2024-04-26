themeToggleBtns = document.querySelectorAll('.theme-toggle-btn');
colorToggleBtns = document.querySelectorAll('.color-toggle-btn');

const body = document.body;
const theme = localStorage.getItem('theme');
const color = localStorage.getItem('color');
const languageSelect = document.getElementById('languageSelect');
const selectedLanguage = localStorage.getItem('language');

// Check if a theme is stored in local storage
if (theme) {
    body.classList.add(theme);
}

if (color) {
    body.classList.add(color);
}

if (selectedLanguage) {
    changeLanguage(selectedLanguage);
    languageSelect.value = selectedLanguage;
}

const themes = ['light', 'dark'];

themeToggleBtns.forEach(btn => {
    btn.addEventListener('click', function () {
        body.classList.toggle('dark');
        // Save the current theme selection to local storage
        localStorage.setItem('theme', body.classList.contains('dark') ? 'dark' : 'light');
    });
});

const colors = ['color-red', 'color-yellow', 'color-orange', 'color-green', 'color-cyan', 'color-blue', 'color-purple'];

colorToggleBtns.forEach(btn => {
    btn.addEventListener('click', function () {
        let currentThemeClass = null;

        // Remove any existing color class
        colors.forEach(color => {
            if (body.classList.contains(color)) {
                body.classList.remove(color);
            }
        });

        // Apply a new random color class
        const newColorClass = colors[Math.floor(Math.random() * colors.length)];
        body.classList.add(newColorClass);

        // Save the current color selection to local storage
        localStorage.setItem('color', newColorClass);
    });
});

// Language select change event listener
languageSelect.addEventListener('change', function () {
    const selectedLang = languageSelect.value;
    changeLanguage(selectedLang);
    // Save the current language selection to local storage
    localStorage.setItem('language', selectedLang);

    updateSelectedOption(selectedLang);
});

function changeLanguage(lang) {
    // Hide all elements with data-lang attribute
    const elements = document.querySelectorAll('[data-lang]');
    elements.forEach(el => {
        el.style.display = 'none';
    });

    // Show the element with the selected language
    const selectedElements = document.querySelectorAll(`[data-lang=${lang}]`);
    selectedElements.forEach(el => {
        if (el.tagName === 'TH') {
            el.style.display = 'table-cell';
        } else {
            el.style.display = 'block';
        }
    });

    // Show buttons with the selected language
    const buttons = document.querySelectorAll('.button');
    buttons.forEach(button => {
        if (button.getAttribute('data-lang') === lang) {
            button.style.display = 'table-cell';
        } else {
            button.style.display = 'none';
        }
    });
}

function updateSelectedOption(lang) {
    const currencySelect = document.getElementById('currency');
    const options = currencySelect.querySelectorAll('option');

    options.forEach(option => {
        option.selected = false;
        if (option.dataset.lang === lang) {
            option.selected = true;
        }
    });
}
