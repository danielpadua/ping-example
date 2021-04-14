# Configurar uma chamada REST para checagem de atributo em uma policy

## Configurar proxy reverso da Test API

1. Faça login em: [https://localhost:5443/console/login](https://localhost:5443/console/) e cadastre um `External Service` para a `Test API` da seguinte maneira:

![Configurar Test API External Server](/img/external-server-config.webp)*Configurar Test API External Server*

2. Depois vá em `Gateway API Endpoints` e mapeie dois endpoints:    

    2.1. `Test API - Get Customer` conforme a seguir:
    ![Configurar Test API com path parameter](/img/gateway-api-endpoint-1.webp)*Configurar Test API com path parameter*

    2.2. `Test API - Customers` conforme a seguir:
    ![Configurar Test API sem path parameter](/img/gateway-api-endpoint-2.webp)*Configurar Test API sem path parameter*

## Configurar os Services necessários

1. Faça login em: [https://localhost:8443/](https://localhost:8443/), vá em `Trust Framework > Services > + > Add new Service` e crie os serviços a seguir:

    1.1. `Test API - Get Customer` conforme a seguir:
    ![Configurar Test API com path parameter](/img/services-1.webp)*Configurar Test API com path parameter*

    1.2. `Test API - Customers` conforme a seguir:
    ![Configurar Test API sem path parameter](/img/services-2.webp)*Configurar Test API sem path parameter*

    1.3. `Get Customer Status` conforme a seguir:
    ![Configurar serviço para obter o status do usuário](/img/services-3.webp)*Configurar serviço para obter o status do usuário*

## Configurar os Atributos necessários

1. Faça login em: [https://localhost:8443/](https://localhost:8443/), vá em `Trust Framework > Attributes > + > Add new Attribute` e crie os atributos a seguir:

    1.1. `Customer Id Claim` conforme a seguir:
    ![Configurar atributo para capturar o id do usuário através do access token](/img/attribute-1.webp)*Configurar atributo para capturar o id do usuário através do access token*

    1.2. `Customer Id Request` conforme a seguir:
    ![Configurar atributo para capturar o id do usuário através da request uri path parameter](/img/attribute-2.webp)*Configurar atributo para capturar o id do usuário através da request uri path parameter*

    1.3. `Customer Status` conforme a seguir:
    ![Configurar atributo para capturar o status do usuário através da Test API (/customer/status/{id})](/img/attribute-3.webp)*Configurar atributo para capturar o status do usuário através da Test API (/customer/status/{id})*

## Configurar as Policies necessárias

1. Faça login em: [https://localhost:8443/](https://localhost:8443/), vá em `Trust Framework > Services` selecione `Global Decision Point` e clique em `+ > Add Policy` e crie os serviços a seguir:

    1.1. `Customer can only check information of his own profile` conforme a seguir:
    ![Configurar policy para que o cliente somente tenha acesso a checar informações do próprio perfil](/img/policy-1.webp)*Configurar policy para que o cliente somente tenha acesso a checar informações do próprio perfil*

    1.2. `Customers can only check information if status is active` conforme a seguir:
    ![Configurar policy para que o cliente somente consiga checar a informação do seu perfil, caso sua conta esteja ativa](/img/policy-2.webp)*Configurar policy para que o cliente somente consiga checar a informação do seu perfil, caso sua conta esteja ativa*

## Testar as políticas

1. No postman verifique os dados da Test API através da chamada a seguir:

![Dados dos clientes da Test API](/img/customers.webp)*Dados dos clientes da Test API*

2. No postman configure um access token para seu ambiente local:
![Access Token possui o id do cliente com o valor 1](/img/access_token.webp)*Access Token possui o id do cliente com o valor 1*

3. Para validar a policy `Customer can only check information of his own profile` monte as requests a seguir utilizando o access token configurado no ambiente local:

    3.1. Esta chamada deve ser permitida pois o id cliente 1 pode acessar as informações referentes a ele mesmo:
    ![Chamada autorizada](/img/allowed-request.webp)*Chamada autorizada*

    3.2. Esta chamada não deve ser permitida pois o id cliente 1 não pode acessar as informações referentes ao id cliente 2:
    ![Chamada não autorizada devido a cliente estar tentando acessar informações de outro cliente](/img/denied-request.webp)*Chamada não autorizada devido a cliente estar tentando acessar informações de outro cliente*

4. Para validar a policy `Customers can only check information if status is active` mude o valor do id cliente do access token de 1 para 2 (o cliente 2 cravado na Test API tem o status definido para `false`), e chame as informações de seu próprio perfil, que deve ser bloqueada pois a conta dele não está ativa, conforme a request a seguir:

![Chamada não autorizada devido a cliente estar com o status false](/img/denied-because-of-status.webp)*Chamada não autorizada devido a cliente estar com o status false*