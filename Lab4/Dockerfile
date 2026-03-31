FROM node:22-alpine

WORKDIR /usr/src/app

COPY app/package.json ./
RUN npm install --only=production

COPY app/server.js ./

ENV PORT=3000

EXPOSE 3000

CMD ["npm", "start"]

