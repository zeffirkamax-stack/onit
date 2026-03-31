const express = require('express');

const app = express();
const port = process.env.PORT || 3000;
const nodeName = process.env.NODE_NAME || 'Неизвестная нода';

app.get('/', (req, res) => {
  res.setHeader('Content-Type', 'text/html; charset=utf-8');
  res.send(`
    <!DOCTYPE html>
    <html lang="ru">
    <head>
      <meta charset="UTF-8" />
      <title>Демо балансировки</title>
      <style>
        body {
          margin: 0;
          padding: 0;
          font-family: system-ui, -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif;
          background: linear-gradient(135deg, #0f172a, #1e293b);
          color: #e5e7eb;
          display: flex;
          align-items: center;
          justify-content: center;
          min-height: 100vh;
        }
        .card {
          background: rgba(15, 23, 42, 0.9);
          border-radius: 16px;
          padding: 32px 40px;
          box-shadow: 0 24px 48px rgba(15, 23, 42, 0.75);
          border: 1px solid rgba(148, 163, 184, 0.4);
          max-width: 480px;
          text-align: center;
        }
        .badge {
          display: inline-flex;
          align-items: center;
          gap: 8px;
          padding: 4px 12px;
          border-radius: 999px;
          background: rgba(56, 189, 248, 0.12);
          color: #7dd3fc;
          font-size: 12px;
          text-transform: uppercase;
          letter-spacing: 0.08em;
          margin-bottom: 16px;
        }
        .badge-dot {
          width: 8px;
          height: 8px;
          border-radius: 999px;
          background: #22c55e;
          box-shadow: 0 0 0 4px rgba(34, 197, 94, 0.25);
        }
        h1 {
          font-size: 40px;
          margin: 0 0 8px;
          color: #e5e7eb;
        }
        .subtitle {
          margin: 0 0 24px;
          color: #9ca3af;
          font-size: 15px;
        }
        .node-name {
          display: inline-flex;
          padding: 8px 18px;
          border-radius: 999px;
          background: radial-gradient(circle at 0 0, #4ade80, #22c55e);
          color: #022c22;
          font-weight: 600;
          font-size: 18px;
          margin-bottom: 8px;
        }
        .hint {
          margin-top: 16px;
          font-size: 13px;
          color: #6b7280;
        }
        code {
          font-family: ui-monospace, SFMono-Regular, Menlo, Monaco, Consolas, "Liberation Mono", "Courier New", monospace;
          padding: 2px 6px;
          border-radius: 6px;
          background: rgba(15, 23, 42, 0.9);
          border: 1px solid rgba(75, 85, 99, 0.7);
          color: #e5e7eb;
        }
      </style>
    </head>
    <body>
      <main class="card">
        <div class="badge">
          <span class="badge-dot"></span>
          <span>Балансировка нагрузки (round-robin)</span>
        </div>
        <h1>${nodeName}</h1>
        <p class="subtitle">
          Эта страница отрендерена одним из backend-контейнеров.
        </p>
        <div class="hint">
          Обновляй страницу — при корректной настройке Nginx будет по очереди
          отправлять запросы на разные контейнеры, и здесь будет
          отображаться <code>Нода 1 / Нода 2 / Нода 3</code>.
        </div>
      </main>
    </body>
    </html>
  `);
});

app.listen(port, () => {
  console.log(`Server started on port ${port}, node: ${nodeName}`);
});

