const container = document.getElementById('container');
const registerBtn = document.getElementById('register');
const loginBtn = document.getElementById('login');

// Handle register and login toggle
registerBtn.addEventListener('click', () => {
    container.classList.add("active");
});

loginBtn.addEventListener('click', () => {
    container.classList.remove("active");
});

// Show password functionality
document.addEventListener('DOMContentLoaded', function () {
    const passwordRegisterInputs = document.getElementsByClassName('passwordRegister');
    const showPasswordRegisterCheckboxes = document.getElementById('showPasswordRegister');
    const passwordLoginInputs = document.getElementsByClassName('passwordLogin');
    const showPasswordLoginCheckboxes = document.getElementById('showPasswordLogin');

    if (showPasswordRegisterCheckboxes) {
        showPasswordRegisterCheckboxes.addEventListener('change', () => {
            Array.from(passwordRegisterInputs).forEach(input => {
                input.type = showPasswordRegisterCheckboxes.checked ? 'text' : 'password';
            });
        });
    }

    if (showPasswordLoginCheckboxes) {
        showPasswordLoginCheckboxes.addEventListener('change', () => {
            // Iterate through all password inputs and toggle their type
            Array.from(passwordLoginInputs).forEach(input => {
                input.type = showPasswordLoginCheckboxes.checked ? 'text' : 'password';
            });
        });
    }
});
