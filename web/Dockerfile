FROM node:18.9.1-alpine3.15 as build
WORKDIR /app

RUN npm install -g @angular/cli

COPY ./package.json .
RUN npm install --force
COPY . .
RUN ng build

FROM nginx as runtime
COPY --from=build /app/dist/core /usr/share/nginx/html