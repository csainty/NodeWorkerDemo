var logentries = require('node-logentries');
var log = logentries.logger({
    userkey: process.env.LOGENTRIES_ACCOUNT_KEY,
    host: process.env.LOGENTRIES_LOCATION.split('/')[0],
    log: process.env.LOGENTRIES_LOCATION.split('/')[1]
});

log.info("NodeWorker Running");
setTimeout(function () {
    process.exit(0);    // Runs once
}, 5000);