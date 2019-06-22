# STEP 1 build static website
FROM node:alpine as builder
RUN apk update && apk add --no-cache make git

# Create app directory
WORKDIR /app

# Install app dependencies
COPY package*.json /app/

# Install quasar-cli
RUN cd /app && npm install @quasar/cli && npm install

# Copy project files into the docker image
COPY . /app

RUN cd /app && quasar build

# STEP 2 build a small nginx image with static website
FROM nginx:alpine

## Remove default nginx website
RUN rm -rf /usr/share/nginx/html/*

## From 'builder' copy website to default nginx public folder
COPY --from=builder /app/dist/spa /usr/share/nginx/html
COPY ./nginx.conf /etc/nginx/nginx.conf
CMD ["nginx", "-g", "daemon off;"]
