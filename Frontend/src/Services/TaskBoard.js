import React, { useEffect, useState } from 'react';
import axios from 'axios';
import Task from '../models/Task';
import TaskCard from '../components/TaskCard';
import TaskForm from '../components/TaskForm';
import {
  statusCodes
} from '../Utils/statusUtils';

// Base URL de ton backend
axios.defaults.baseURL = 'https://localhost:7279';


function TaskBoard() {
  const [tasks, setTasks] = useState([]);
  const [message, setMessage] = useState('');

  // Chargement des tâches
  useEffect(() => {
    axios.get('/Tasks')
      .then((res) => {
        const loadedTasks = res.data.map((t) => new Task(t));
        setTasks(loadedTasks);
      })
      .catch(() => setMessage("Erreur de chargement des tâches"));
  }, []);

  // Changement de statut
  const handleStatusChange = async (task, nextStatusLabel) => {
    const nextStatusCode = statusCodes[nextStatusLabel];

    try {
      await axios.put(`/Tasks/${task.id}/status`, {
        newStatus: nextStatusCode,
        changedByUserId: 1
      });

      const res = await axios.get(`/Tasks/${task.id}`);
      const updatedTask = res.data;
      console.log(updatedTask)
      updatedTask.status = Number(updatedTask.status);
      
      const updatedTasks = tasks.map((t) =>
        t.id === task.id ? updatedTask : t
      );
      setTasks(updatedTasks);
      setMessage("Statut mis à jour !");
    } catch (err) {
      console.error(err);
      setMessage("Erreur de mise à jour du statut");
    }
  };

  // Affichage par colonne
  const renderColumn = (statusLabel) => {
    const statusCode = statusCodes[statusLabel];
    const filteredTasks = tasks.filter((t) => t.status === statusCode);
    return (
      <div style={{ flex: 1 }}>
        <h3>{statusLabel}</h3>
        {filteredTasks.map((t) => (
          <TaskCard key={t.id} task={t} onStatusChange={handleStatusChange} />
        ))}
      </div>
    );
  };

  return (
    <div>
      <h2>Tableau de Tâches</h2>
      {message && <p>{message}</p>}
      <TaskForm onTaskCreated={(newTask) => setTasks([...tasks, newTask])} />
      <div style={{ display: 'flex', gap: 16 }}>
        {['ToDo', 'InProgress', 'Done'].map(renderColumn)}
      </div>
    </div>
  );
}

export default TaskBoard;
