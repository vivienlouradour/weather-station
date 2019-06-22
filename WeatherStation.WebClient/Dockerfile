# develop stage
FROM node:11.1-alpine as develop-stage
WORKDIR /app
COPY package*.json ./
RUN yarn global add quasar-cli
COPY . .

# build stage
FROM develop-stage as build-stage
RUN yarn
RUN quasar build

# production stage
FROM nginx:1.15.7-alpine as production-stage
COPY --from=build-stage /app/dist/spa-mat /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]