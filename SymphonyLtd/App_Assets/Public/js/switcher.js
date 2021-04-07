// Swicher
function toggleSwitcher() {
    var i = document.getElementById('style-switcher');
    if (i.style.left === "-189px") {
        i.style.left = "0px";
    } else {
        i.style.left = "-189px";
	}
};

function setColor(theme) {
    var url = '../css/colors/' + theme + '.css';
    document.getElementById('color-opt').href = url;
    toggleSwitcher(false);
};

function setTheme(theme) {
    var url = '../css/' + theme + '.min.css';
    document.getElementById('theme-opt').href = url;
    toggleSwitcher(false);
};
