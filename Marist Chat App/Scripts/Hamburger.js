function hamburger(x) {
    x.classList.toggle("change");
    var menuIcon = document.getElementById('menuIcon');
    if (menuIcon.style.display == "block") {
        menuIcon.style.display = "none";
    } else {
        menuIcon.style.display = "block";
    }
}