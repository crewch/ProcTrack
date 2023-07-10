FROM node:18.15.0 as buildArtifacts
COPY . .
RUN yarn install --prefer-offline
RUN yarn --prefer-offline && yarn build

FROM nginx:1.11.12
COPY nginx/default.conf /etc/nginx/conf.d/default.conf
COPY --from=buildArtifacts /dist /srv/www/web
CMD ["nginx", "-g", "daemon off;"]