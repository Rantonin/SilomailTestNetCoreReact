import React from 'react';
import {
  statusLabels
} from '../Utils/statusUtils';

function TaskCard({ task, onStatusChange }) {
  const { title, description, status } = task;

  const getNextStatus = () => {
    if (status === 0) return 'InProgress';
    if (status === 1) return 'Done';
    return null;
  };

  const nextStatus = getNextStatus();

  return (
    <div style={{ border: '1px solid #ccc', padding: 12, marginBottom: 8 }}>
      <h4>{title}</h4>
      {description && <p>{description}</p>}
      <p><strong>Status :</strong> {statusLabels[status]}</p>
      {nextStatus && (
        <button onClick={() => onStatusChange(task, nextStatus)}>
          {status === 0 ? 'Start' : 'Complete'}
        </button>
      )}
    </div>
  );
}

export default TaskCard;
