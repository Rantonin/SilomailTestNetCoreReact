export default class Task {
  constructor(data) {
    this.id = data.id;
    this.title = data.title;
    this.description = data.description;
    this.status = data.status;
    this.assignedToUserId = data.assignedToUserId;
    this.createdAt = data.createdAt;
    this.updatedAt = data.updatedAt;
  }

  isCompleted() {
    return this.status === 'Done';
  }

  getNextStatus() {
    if (this.status === 'ToDo') return 'InProgress';
    if (this.status === 'InProgress') return 'Done';
    return null;
  }
}
