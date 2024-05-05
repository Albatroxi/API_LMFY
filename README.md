# Documentação da API

#### Criação de usuário

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

```http
  POST /api/users/resetPass
```
| Parâmetro        | Tipo       | Descrição                                                 |
| :--------------- | :-------   | :---------------------------------------------------      |
| `email`          | `string`   | **Obrigatório**. Informar o email                         |

# Descrição de atividades - Reset de Senha

## Problemas

<details><summary>Ao solicitar o reset de senha</summary>
    <p>
        <table>
            <tbody>
                <tr>
                    <th style="width: 25%;">
                        Problema
                    </th>
                    <td>
                        Ausência de controle das informações da entidade 
                    </td>
                </tr>
                <tr>
                    <th style="width: 25%;">
                        Afeta
                    </th>
                    <td>
                        Entidade/Diretoria/Funcionários
                    </td>
                </tr>
                <tr>
                    <th style="width: 25%;">
                        Impacto
                    </th>
                    <td>
                        Dificuldade em atender as exigências da LGPD pela ausência de segmentação no controle de acesso.
                    </td>
                </tr>
                <tr>
                    <th style="width: 25%;">
                        Solução
                    </th>
                    <td>
                        Virtualização dos dados e restrição de acesso às informações através de do sistema via autenticação e autorização dos usuários.
                    </td>
                </tr>
            </tbody>
        </table>
    </p>
</details>

#### Breve descrição das bibliotecas utilizadas

| Informação                                 | Descrição                                                 |
| :------------------------------------------| :-------------------------------------------------------- |
| `Microsoft.EntityFrameworkCore`            | **Descrição 1**.                                          |
| `Microsoft.EntityFrameworkCore.Tools`      | **Descrição 2**.                                          |
| `Microsoft.EntityFrameworkCore.Design`     | **Descrição 3**.                                          |
| `Pomelo.EntityFrameworkCore.Mysql`         | **Descrição 4**.                                          |
| `System.Net.Mail`                          | **Descrição 5**.                                          |


