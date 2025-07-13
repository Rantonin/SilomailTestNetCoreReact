import React, { useState } from 'react';
import axios from 'axios';
import Task from '../models/Task';

const CREATED_BY_USER_ID = 1;

function TaskForm({ onTaskCreated }) {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const res = await axios.post('/Tasks', {
        title,
        description,
        createdByUserId: CREATED_BY_USER_ID,
      });

      const newTask = new Task(res.data);
      onTaskCreated(newTask);
      setTitle('');
      setDescription('');
    } catch (err) {
      console.error("Erreur lors de la création de la tâche :", err);
    }
  };

  return (
    <form onSubmit={handleSubmit} style={{ marginBottom: 24 }}>
      <input
        type="text"
        placeholder="Titre"
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        required
      />
      <input
        type="text"
        placeholder="Description (optionnelle)"
        value={description}
        onChange={(e) => setDescription(e.target.value)}
        style={{ marginLeft: 8 }}
      />
      <button type="submit" style={{ marginLeft: 8 }}>Ajouter</button>
    </form>
  );
}

export default TaskForm;
