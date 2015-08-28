document.onkeydown = checkKey;
maxpage = 11
function checkKey(e) {
    e = e || window.event;
    
    var url = window.location.pathname;
    var prev = "", next = "", up = "", down = "";
    var filename = url.substring(url.lastIndexOf('/') + 1);
    var arr = filename.split(".", 3);
    if (filename == "fastleet" || filename == "" || filename == "index.html") {
        prev = "index.html", next = "1.question.html", up = "index.html", down = "index.html";
    }
    else {
        var pagenumber = parseInt(arr[0]);
        prev = pagenumber - 1 == 0 ? "index.html" : (pagenumber - 1).toString() + ".question.html";
        next = pagenumber + 1 <= maxpage ? (pagenumber + 1).toString() + ".question.html" : "index.html";
        up = pagenumber.toString() + ".question.html";
        down = pagenumber.toString() + ".solution.html";
    }
    if (e.keyCode == '38') {
        window.location.href = up;
    } else if (e.keyCode == '40') {
        window.location.href = down;
    } else if (e.keyCode == '37') {
        window.location.href = prev;
    } else if (e.keyCode == '39') {
        window.location.href = next;
    }
}
