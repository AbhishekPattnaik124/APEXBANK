import * as signalR from "@microsoft/signalr";

class NotificationService {
  constructor() {
    this.connection = null;
    this.callbacks = [];
  }

  async start() {
    if (this.connection) return;

    this.connection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5000/hubs/notifications")
      .withAutomaticReconnect()
      .build();

    this.connection.on("ReceiveNotification", (message) => {
      this.callbacks.forEach(cb => cb(message));
    });

    try {
      await this.connection.start();
      console.log("SignalR Connected.");
    } catch (err) {
      console.log("SignalR Connection Error: ", err);
    }
  }

  onNotification(callback) {
    this.callbacks.push(callback);
  }
}

const notificationService = new NotificationService();
export default notificationService;
