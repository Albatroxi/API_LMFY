# Documentação da API


## Criação de usuário

Ao registrar um usuário.

```http
  POST /api/users/register
```
| Parâmetro        | Tipo       | Descrição                                                 |
| :--------------- | :-------   | :---------------------------------------------------      |
| `nome`           | `string`   | **Obrigatório**. Informar o nome do usuário               |
| `email`          | `string`   | **Obrigatório**. Informar o email - _É chave primária_    |
| `pssW`           | `string`   | **Obrigatório**. Informar a senha - _Mínimo 6 dígitos_    |
| `attribute`      | `integer`  | **Não Obrigatório**. Flag de definição do tipo de usuário |

Parâmetro - attribute - é definido o tipo de usuário para referencias as permições de ações de cada usuário:

* 0 - Alunos
* 1 - Professores
* 2 - Administradores
* 3 - Desenvolvedores

## Perdi a senha

Ao solicitar o reset da senha / Perdi a senha.
```http
  POST /api/users/resetPass
```
| Parâmetro        | Tipo       | Descrição                                                 |
| :--------------- | :-------   | :---------------------------------------------------      |
| `email`          | `string`   | **Obrigatório**. Informar o email                         |

# Descrição de atividades - Reset de Senha

<details><summary>Solicitar nova senha - Perdi a senha</summary>
    <p>
        <table>
            <tbody>
                <tr>
                    <th style="width: 25%;">
                        E-mail.
                    </th>
                    <td>
                        É necessário informar o e-mail do usuário.
                    </td>
                </tr>
                <tr>
                    <th style="width: 25%;">
                        FindAsync(email)
                    </th>
                    <td>
                        Verifica se existe usuário cadastrado com o e-mail informado.
                    </td>
                </tr>
                <tr>
                    <th style="width: 25%;">
                        gnewpass
                    </th>
                    <td>
                        Gera uma nova senha aleatória, capturando os 8 primeiros dígitos.
                    </td>
                </tr>
                <tr>
                    <th style="width: 25%;">
                        SendMail
                    </th>
                    <td>
                        Envia um e-mail, para o e-mail informado.
                    </td>
                </tr>
                <tr>
                    <th style="width: 25%;">
                        SMTP
                    </th>
                    <td>
                        smtp-mail.outlook.com
                </td>
                                  <tr>
                    <th style="width: 25%;">
                        Porta
                    </th>
                    <td>
                        587
                </td>
                </tr>
            </tbody>
        </table>
    </p>
</details>

## Realizar login

```http
  POST /api/users/resetPass
```
| Parâmetro        | Tipo       | Descrição                                                 |
| :--------------- | :-------   | :---------------------------------------------------      |
| `email`          | `string`   | **Obrigatório**. Informar o email                         |
| `passW`          | `string`   | **Obrigatório**. Informar a senha                         |

# Descrição de atividades - Reset de Senha

<details><summary>Solicitar nova senha - Perdi a senha</summary>
    <p>
        <table>
            <tbody>
                <tr>
                    <th style="width: 25%;">
                        E-mail.
                    </th>
                    <td>
                        É necessário informar o e-mail do usuário.
                    </td>
                </tr>
                <tr>
                    <th style="width: 25%;">
                        Paswword
                    </th>
                    <td>
                        É necessário informar a senha do usuário.
                    </td>
                </tr>
            </tbody>
        </table>
    </p>
</details>

# Diagrama de atividades 
#### _Atualizado conforme o andamento do desenvolvimento_

![image](https://github.com/Albatroxi/API_LMFY/assets/167586363/e0a39a25-c36c-4993-9c53-5982dc224be8)


#### Breve descrição das bibliotecas utilizadas

| Informação                                 | Descrição                                                 |
| :------------------------------------------| :-------------------------------------------------------- |
| `Microsoft.EntityFrameworkCore`            | **Descrição 1**.                                          |
| `Microsoft.EntityFrameworkCore.Tools`      | **Descrição 2**.                                          |
| `Microsoft.EntityFrameworkCore.Design`     | **Descrição 3**.                                          |
| `Pomelo.EntityFrameworkCore.Mysql`         | **Descrição 4**.                                          |
| `System.Net.Mail`                          | **Descrição 5**.                                          |


