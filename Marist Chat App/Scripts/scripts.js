﻿function NewStream(div, btn) {
    HideShow(div);
    var usrClass = btn.value.replace('NEW STREAM IN ', '');
    document.getElementById('BodyContent_txtStreamID').value = usrClass;
}

function button_click(e) {
    var keyCode = e.keyCode ? e.keyCode : e.which;
    console.log("WINDOW EVENT KEYCODE: " + keyCode);
    if (keyCode == 13) {
        document.getElementById('BodyContent_btnSend').focus();
        console.log("focused")
        document.getElementById('BodyContent_btnSend').click();
        console.log("sent")
    }
}

function ScrollDown() {
    var objDiv = document.getElementById("BodyContent_pnlMessages");
    objDiv.scrollTop = objDiv.scrollHeight;
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

function collapse(label) {
    var ancestor = document.getElementsByClassName('sidebar')[0],
        descendents = ancestor.getElementsByTagName('*');
    for (i = 0; i < descendents.length; i++) {
        if (descendents[i] = label) {
            console.log("label at " + i);
            //todo: finish this when the website comes back online

            //get all elements in DOM until next <li>

            //set their styles to display:none or display:block respectively
        }
    }
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