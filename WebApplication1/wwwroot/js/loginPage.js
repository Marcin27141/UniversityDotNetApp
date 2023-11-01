const containerElement = document.getElementById('top-login-container');
const forgotPasswordElement = document.getElementById('forgot-password-text');
const backButton = document.getElementById('back-button');

forgotPasswordElement.addEventListener('click', () => {
    console.log("clicked");
    containerElement.classList.add('active');
})

backButton.addEventListener('click', () => {
    containerElement.classList.remove('active');
})