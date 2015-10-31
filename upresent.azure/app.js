var express = require('express');
var app = express();

app.use(express.static('public'));
var maxpage = 221;
var excluded = {
    18: '', 
    21: '', 
    36: '', 
    52: '', 
    68: '', 
    156: '', 
    157: '', 
    158: '', 
    159: '', 
    161: '', 
    163: '', 
    167: '', 
    170: '', 
    175: '', 
    176: '', 
    177: '', 
    178: '', 
    180: '', 
    181: '', 
    182: '', 
    183: '', 
    184: '', 
    185: '', 
    186: '', 
    188: '', 
    189: '', 
    191: '', 
    192: '', 
    193: '', 
    194: '', 
    195: '', 
    196: '', 
    197: '', 
    203: '', 
    204: '', 
    206: '', 
    210: '', 
    218: ''
};

var commands = { 'q': '.question.html', 's': '.solution.html' };

function checkQuestionId(questionid) {
    while (questionid in excluded) {
        questionid = (questionid - 1 + maxpage) % maxpage;
    }
    return questionid;
}

app.get('/', function (req, res) {
    res.writeHead(200, { 'Content-Type': 'text/plain' });
    res.end('U Presents\n');
});

app.get('/fastleet/', function (req, res) {
    res.sendFile('index.html', { root: __dirname + '/public/fastleet' });
});

app.get('/fastleet/:questionid', function (req, res) {
    var questionid = checkQuestionId(req.params.questionid);
    var questionHtml = questionid + commands['q'];
    if (questionid < 1) {
        questionHtml = "index.html";
    }
    res.sendFile(questionHtml, { root: __dirname + '/public/fastleet' });
});

app.get('/fastleet/:questionid/:command', function (req, res) {
    var questionid = checkQuestionId(req.params.questionid);
    var questionHtml = questionid + commands[req.params.command];
    if (req.params.questionid < 1) {
        questionHtml = "index.html";
    }
    res.sendFile(questionHtml, { root: __dirname + '/public/fastleet' });
});

var server = app.listen(1337, function () {
    var host = server.address().address;
    var port = server.address().port;
    
    console.log('Example app listening at http://%s:%s', host, port);
});


module.exports = app;