﻿document.onkeydown = checkKey;
var maxpage = 221;
var commands = ['q', 's'];
var pageMapping = { 'q': 0, 's': 1 };
var fastleet = "fastleet";
var oldScrollY = 0;
var oldScrollX = 0;

function max(a, b) { return a >= b ? a : b; }
function min(a, b) { return a <= b ? a : b; }

function checkKey(e) {
    e = e || window.event;

    var keyNum = parseInt(e.keyCode);
    if (keyNum >= 37 && keyNum <= 40) {
    var url = window.location.pathname;   
    var segments = url.split('/');
    var idx = segments.indexOf(fastleet);
    if (idx >= 0) {
            var prev = "", next = "", up = "", down = "", pagenumber = 0, cmd = 'q';
            if (idx < segments.length - 1) {
                pagenumber = segments[idx + 1];
            }
            if (idx < segments.length - 2) {
                cmd = segments[idx + 2];
            }
        
            if (pagenumber && pagenumber.length > 0) {
                var temp = (pagenumber - 1 + maxpage) % maxpage;
                prev = "fastleet/" + temp + "/q";
                temp = (parseInt(pagenumber) + 1) % maxpage;
                next = "fastleet/" + temp + '/q';
                up = "fastleet/" + pagenumber + '/' + commands[max(pageMapping[cmd] - 1, 0)];
                down = "fastleet/" + pagenumber + '/' + commands[min(pageMapping[cmd] + 1, commands.length - 1)];           
            } else {
                prev = fastleet;
                next = fastleet + "/1";
                up = fastleet;
                down = fastleet;
            }
            
            var keyHandler = { '37': prev, '38': up, '39': next, '40': down };

        

            if (parseInt(e.keyCode) % 2) {
                window.scrollBy(5, 0);
               // alert(window.scrollX);
                if (window.scrollX == oldScrollX) {
                   window.location.pathname = keyHandler[e.keyCode];
                }
                oldScrollX = window.scrollX;
            }
            else {
                window.scrollBy(0, 5);
                if (window.scrollY == oldScrollY) {
                    window.location.pathname = keyHandler[e.keyCode];
                }
                oldScrollY = window.scrollY;
            }
        }
    }
}
