var http = require('http');
var express = require('express');
var qs = require("querystring");

var app = express();
var bodyParser = require('body-parser');

// Create application/x-www-form-urlencoded parser
var urlencodedParser = bodyParser.urlencoded({ extended: false })

app.get('/index.htm', function (req, res) {
	res.sendFile( __dirname + "/" + "index.htm" );
})

app.post('/process_post', urlencodedParser, function (req, res) {
	// Prepare output in JSON format
	response = {
	   first_name:req.body.first_name,
	   last_name:req.body.last_name
	};
	
	res.setHeader('Access-Control-Allow-Origin', 'http://localhost:58483/');
	res.writeHead(200, {'Content-Type': 'text/plain'})
	res.end(JSON.stringify(response));
})

app.get('/rajaongkir/province/*', function(req, res){
	var requrl = req.url;
	requrl = requrl.replace('/rajaongkir/province/','/starter/province?id=');
	
	var options = {
	  "method": "GET",
	  "hostname": "api.rajaongkir.com",
	  "port": null,
	  "path": requrl,
	  "headers": {"key": "46f7b4335b54bf8e970eba4815429fa9"}
	};
	
	var request = http.request(options, function (response) {
		var chunks = [];

		response.on("data", function (chunk) {
			chunks.push(chunk);
		});
		
		response.on("end", function () {
			var body = Buffer.concat(chunks);
			res.setHeader('Access-Control-Allow-Origin', '*');
			res.writeHead(200, {'Content-Type': 'application/json'})
			res.end(body);
		});
	});
	request.end();
});

app.get('/rajaongkir/city/id=*&province=*', function(req, res) {
	var requrl = req.url;
	requrl = requrl.replace('/rajaongkir/city/','/starter/city?');
	
	var options = {
	  "method": "GET",
	  "hostname": "api.rajaongkir.com",
	  "port": null,
	  "path": requrl,
	  "headers": {"key": "46f7b4335b54bf8e970eba4815429fa9"}
	};
	
	var request = http.request(options, function (response) {
		var chunks = [];

		response.on("data", function (chunk) {
			chunks.push(chunk);
		});
		
		response.on("end", function () {
			var body = Buffer.concat(chunks);
			res.setHeader('Access-Control-Allow-Origin', '*');
			res.writeHead(200, {'Content-Type': 'application/json'})
			res.end(body);
		});
	});
	request.end();
})

app.post('/rajaongkir/cost/', urlencodedParser, function(req, res){
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
	
	var request = http.request(options, function (response) {
		var chunks = [];

		response.on("data", function (chunk) {
			chunks.push(chunk);
		});
		
		response.on("end", function () {
			var body = Buffer.concat(chunks);
			res.setHeader('Access-Control-Allow-Origin', '*');
			res.writeHead(200, {'Content-Type': 'application/json'})
			res.end(body);
		});
	});
	request.write(qs.stringify({ origin: req.body.origin, destination: req.body.destination, weight: req.body.weight, courier: req.body.courier }));
  
	request.end();
});

var server = app.listen(8081, function () {

  var host = server.address().address
  var port = server.address().port

  console.log("Example app listening at http://%s:%s", host, port)

})