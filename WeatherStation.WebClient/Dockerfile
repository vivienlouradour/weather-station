# STEP 1 build static website
FROM node:alpine as builder
RUN apk update && apk add --no-cache make git

# Create app directory
WORKDIR /app

# Install app dependencies
COPY package*.json /app/

# Install quasar-cli
RUN cd /app && npm install @quasar/cli

# Copy project files into the docker image
COPY . /app

RUN cd /app && quasar build

# STEP 2 build a small nginx image with static website
FROM nginx:alpine

## Remove default nginx website
RUN rm -rf /usr/share/nginx/html/*

## From 'builder' copy website to default nginx public folder
COPY --from=builder /app/dist/spa-mat /usr/share/nginx/html
COPY ./nginx.conf /etc/nginx/nginx.conf
CMD ["nginx", "-g", "daemon off;"]

# develop stage
#FROM node:11.1-alpine as develop-stage
#WORKDIR /app
#COPY package*.json ./
#RUN yarn global add quasar-cli
#COPY . .
#
## build stage
#FROM develop-stage as build-stage
#RUN yarn
#RUN quasar build
#
## production stage
#FROM nginx:1.15.7-alpine as production-stage
#COPY --from=build-stage /app/dist/spa-mat /usr/share/nginx/html
#EXPOSE 80
#CMD ["nginx", "-g", "daemon off;"]