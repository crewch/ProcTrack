FROM nginx
COPY ./nginx.conf /etc/nginx/nginx.conf
# COPY ./frontend/frontend-app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]