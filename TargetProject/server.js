const express = require("express");
const bodyParser = require("body-parser");

const app = express();
const PORT = 3000;

// Middleware to parse URL-encoded bodies
app.use(bodyParser.urlencoded({ extended: true }));

// Endpoint to handle the fetch request
app.post("/api/test", (req, res) => {
  console.log("Request received:");
  console.log("Headers:", req.headers);
  console.log("Body:", req.body);

  // Simulate processing logic
  if (req.body === "test") {
    return res.status(200).send("Request received and processed successfully.");
  }

  res.status(400).send("Invalid data.");
});

// Start the server
app.listen(PORT, () => {
  console.log(`Server is running at http://localhost:${PORT}`);
});