# Configurar uma chamada REST para checagem de atributo em uma policy

## Configurar proxy reverso da Test API

1. Faça login em: [https://localhost:5443/console/login](https://localhost:5443/console/) e cadastre um `External Service` da seguinte maneira:

![Configurar External Server](/img/external-server-config.webp)

2. Depois vá em `Gateway API Endpoints` e mapeie dois endpoints:    
    2.1. `Test API - Customers` conforme a seguir:
    ![Configurar External Server](/img/gateway-api-endpoint-1.webp)

    2.2. `Test API - Get Customer` conforme a seguir:
    ![Configurar External Server](/img/gateway-api-endpoint-2.webp)