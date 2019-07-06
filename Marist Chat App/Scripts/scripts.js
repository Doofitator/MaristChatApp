function NewStream(div, btn) {
    HideShow(div);
    var usrClass = btn.value.replace('NEW STREAM IN ', '');
    document.getElementById('BodyContent_txtStreamID').value = usrClass;
}

function button_click(objTextBox, objBtnID) {
    if (window.event.keyCode == 13) {
        document.getElementById(objBtnID).focus();
        document.getElementById(objBtnID).click();
    }
}


function HideShow(div) {
    var element = document.getElementById(div);
    if (element.style.display == "block") {
        element.style.display = "none";
    } else {
        element.style.display = "block";
    }
    dragElement(element);
}

// from https://www.w3schools.com/howto/howto_js_draggable.asp:

function dragElement(elmnt) {
    var pos1 = 0, pos2 = 0, pos3 = 0, pos4 = 0;
    if (document.getElementById(elmnt.id + "TitleBar")) {
        // if present, the header is where you move the DIV from:
        document.getElementById(elmnt.id + "TitleBar").onmousedown = dragMouseDown;
    }

    function dragMouseDown(e) {
        e = e || window.event;
        e.preventDefault();
        // get the mouse cursor position at startup:
        pos3 = e.clientX;
        pos4 = e.clientY;
        document.onmouseup = closeDragElement;
        // call a function whenever the cursor moves:
        document.onmousemove = elementDrag;
    }

    function elementDrag(e) {
        e = e || window.event;
        e.preventDefault();
        // calculate the new cursor position:
        pos1 = pos3 - e.clientX;
        pos2 = pos4 - e.clientY;
        pos3 = e.clientX;
        pos4 = e.clientY;
        // set the element's new position:
        elmnt.style.top = (elmnt.offsetTop - pos2) + "px";
        elmnt.style.left = (elmnt.offsetLeft - pos1) + "px";
    }

    function closeDragElement() {
        // stop moving when mouse button is released:
        document.onmouseup = null;
        document.onmousemove = null;
    }
}