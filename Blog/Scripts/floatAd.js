var xPos = 300;
var yPos = 200;
var step = 1;
var delay = 30;
var height = 0;
var Hoffset = 0;
var Woffset = 0;
var yon = 0;
var xon = 0;
var pause = true;
var interval;


function ad() {
    this.items = [];
    this.addItem = function (content) {
        //width:59px;height:61px;
        document.write('<div id="img1" style="z-index:1000;left:2px;position:absolute;top:43px;'
         + 'visibility:visible;" onmouseenter="pause_resume(1);" onmouseleave="pause_resume(0);">' + content + '</div>');
    }
    this.play = function () {
        start();
    }
}
//document.getElementById('img1').style.top = yPos;


function changePos() {
    width = document.documentElement.clientWidth;
    height = document.documentElement.clientHeight;
    Hoffset = img1.offsetHeight;
    Woffset = img1.offsetWidth;
    img1.style.left = xPos + document.documentElement.scrollLeft + "px";
    img1.style.top = yPos + document.documentElement.scrollTop + "px";

    if (yon) {
        yPos = yPos + step;
    } else {
        yPos = yPos - step;
    }
    if (yPos < 0) {
        yon = 1;
        yPos = 0;
    }
    if (yPos >= (height - Hoffset)) {
        yon = 0; yPos = (height - Hoffset);
    }
    if (xon) {
        xPos = xPos + step;
    } else {
        xPos = xPos - step;
    }
    if (xPos < 0) {
        xon = 1; xPos = 0;
    }
    if (xPos >= (width - Woffset)) {
        xon = 0; xPos = (width - Woffset);
    }
}

function start() {
    img1.visibility = "visible";
    interval = setInterval('changePos()', delay);
}

function pause_resume(pause) {
    if (pause == 1) {
        clearInterval(interval);
        pause = false;
    } else {
        interval = setInterval('changePos()', delay);
        pause = true;
    }
}