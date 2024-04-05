const themeToggleBtn = document.getElementById('themeToggleBtn');
const body = document.body;
const theme = localStorage.getItem('theme');

// Check if a theme is stored in local storage
if (theme) {
    body.classList.add(theme);
}

themeToggleBtn.addEventListener('click', function () {
    body.classList.toggle('dark');
    // Save the current theme selection to local storage
    localStorage.setItem('theme', body.classList.contains('dark') ? 'dark' : 'light');
});