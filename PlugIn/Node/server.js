var http = require('http');
var express = require('express');
var qs = require("querystring");
var bodyParser = require('body-parser');
var app = express();

// Create application/x-www-form-urlencoded parser
app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());

app.get('/rajaongkir/province/*', function (req, res) {
    var requrl = req.url;
    requrl = requrl.replace('/rajaongkir/province/', '/starter/province?id=');
    res.setHeader('Access-Control-Allow-Origin', '*');
    var options = {
        "method": "GET",
        "hostname": "api.rajaongkir.com",
        "port": null,
        "path": requrl,
        "headers": { "key": "46f7b4335b54bf8e970eba4815429fa9" }
    };
    try {
        var request = http.request(options, function (response) {
            var chunks = [];

            response.on("data", function (chunk) {
                chunks.push(chunk);
            });

            response.on("end", function () {
                var body = Buffer.concat(chunks);
                res.writeHead(200, { 'Content-Type': 'application/json' })
                res.end(body);
            });
        });
        request.end();
    }
    catch (ex) {
        //        res.writeHead(500, { 'Content-Type': 'application/json' });
        //        var temp = { ok: false, message: "Fail : " + ex.toString() };
        //        res.end(temp);
        console.log(ex.toString());
    }
});

app.get('/rajaongkir/city/id=*&province=*', function (req, res) {
    var requrl = req.url;
    requrl = requrl.replace('/rajaongkir/city/', '/starter/city?');
    res.setHeader('Access-Control-Allow-Origin', '*');
    var options = {
        "method": "GET",
        "hostname": "api.rajaongkir.com",
        "port": null,
        "path": requrl,
        "headers": { "key": "46f7b4335b54bf8e970eba4815429fa9" }
    };
    try {
        var request = http.request(options, function (response) {
            var chunks = [];

            response.on("data", function (chunk) {
                chunks.push(chunk);
            });

            response.on("end", function () {
                var body = Buffer.concat(chunks);
                res.writeHead(200, { 'Content-Type': 'application/json' })
                res.end(body);
            });
        });
        request.end();
    }
    catch (ex) {
        res.writeHead(500, { 'Content-Type': 'application/json' });
        var temp = { ok: false, message: "Fail : " + ex.toString() };
        res.end(temp);
    }
})

app.post('/rajaongkir/cost/', function (req, res) {
    res.setHeader('Access-Control-Allow-Origin', '*');
    var options = {
        "method": "POST",
        "hostname": "api.rajaongkir.com",
        "port": null,
        "path": "/starter/cost",
        "headers": {
            "key": "46f7b4335b54bf8e970eba4815429fa9",
            "content-type": "application/x-www-form-urlencoded"
        }
    };
    try {
        var request = http.request(options, function (response) {
            var chunks = [];

            response.on("data", function (chunk) {
                chunks.push(chunk);
            });

            response.on("end", function () {
                var body = Buffer.concat(chunks);
                res.writeHead(200, { 'Content-Type': 'application/json' })
                res.end(body);
            });
        });
        request.write(qs.stringify({ origin: req.body.origin, destination: req.body.destination, weight: req.body.weight, courier: req.body.courier }));
        request.end();
    }
    catch (ex) {
        console.log(ex.toString());
    }
});

app.post('/Test/', function (req, res) {
    console.log('masuk');
    console.log(req.body);
    res.end();
});

var server = app.listen(8081, function () {
    var host = server.address().address;
    var port = server.address().port;
    console.log("Example app listening at http://%s:%s", host, port);
});