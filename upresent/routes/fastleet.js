﻿var express = require('express');
var router = express.Router();

/* GET users listing. */
router.get('/', function (req, res) {
    res.send('/public/fastleet/index.html');
});

module.exports = router;