var logentries = require('node-logentries');
var log = logentries.logger({
	userkey:'92e4a151-9424-4b1b-beff-b662cb918757',
	host: 'AppHarbor',
	log: 'Default'
});

log.info("NodeWorker Running");
setTimeout(function() {
	process.exit(100);
}, 5000);
