themeToggleBtns = document.querySelectorAll('.theme-toggle-btn');
const body = document.body;
const theme = localStorage.getItem('theme');
const languageSelect = document.getElementById('languageSelect');
const selectedLanguage = localStorage.getItem('language');

// Check if a theme is stored in local storage
if (theme) {
    body.classList.add(theme);
}

// Check if a language is stored in local storage
if (selectedLanguage) {
    changeLanguage(selectedLanguage);
    languageSelect.value = selectedLanguage;
}

// Theme toggle button event listener
themeToggleBtns.forEach(btn => {
    btn.addEventListener('click', function () {
        body.classList.toggle('dark');
        // Save the current theme selection to local storage
        localStorage.setItem('theme', body.classList.contains('dark') ? 'dark' : 'light');
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
