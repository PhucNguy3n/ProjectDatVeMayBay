const container = document.getElementById('container');
const registerBtn = document.getElementById('register');
const loginBtn = document.getElementById('login');

registerBtn.addEventListener('click', () => {
    container.classList.add("active");
});

loginBtn.addEventListener('click', () => {
    container.classList.remove("active");
});

//Show password
const passwordInput = document.getElementById('password'); // assuming you have an element with id="password"
const showPasswordCheckbox = document.getElementById('showPassword');

showPasswordCheckbox.addEventListener('change', () => {
    passwordInput.type = showPasswordCheckbox.checked ? 'text' : 'password';
});
