# Documentação da API

<details><summary><strong>Usuarios</strong></summary>

  ## Criação de usuário


Ao registrar um usuário.

```http
  POST /api/usuarios/registrarUsuario
```
| Parâmetro        | Tipo       | Descrição                                                 |
| :--------------- | :-------   | :---------------------------------------------------      |
|`nome completo`   | `string`   | **Obrigatório**. Informar o nome completo do usuário      |
| `email`          | `string`   | **Obrigatório**. Informar o email - _É chave primária_    |
| `senha`          | `string`   | **Obrigatório**. Informar a senha - _Mínimo 6 dígitos_    |
| `perfil`         | `string`   | **Obrigatório**. Flag de definição do tipo de usuário |

Parâmetro - Perfil- é definido o tipo de usuário para referencias as permições de ações de cada usuário:

* 0 - Alunos
* 1 - Professores
* 2 - Administradores
* 3 - Desenvolvedores

  
## Login com Usuario

> IMPORTANT!
>
>É nessesario ja estar registrado no sistema

```http
    POST /api/usuarios/loginAction
```
| Parâmetro        | Tipo       | Descrição                                                 |
| :--------------- | :-------   | :---------------------------------------------------      |
| `email`          | `string`   | **Obrigatório**. Informar o email                         |
| `senha`          | `string`   | **Obrigatório**. Informar a senha                         |


## Esqueci a senha

Ao solicitar o reset da senha / Perdi a senha.
```http
  POST /api/usuarios/esqueciasenhaUsuario
```
| Parâmetro        | Tipo       | Descrição                                                 |
| :--------------- | :-------   | :---------------------------------------------------      |
| `email`          | `string`   | **Obrigatório**. Informar o email                         |
</details> 
  
<hr>

<details><summary><strong>Cursos</strong></summary>
  
## Cadastrar Cursos

Ao cadastrar curso.
```http
  POST /api/cursos/cadastrarCursos
```
| Parâmetro        | Tipo        | Descrição                                                 |
| :--------------- | :-------    | :---------------------------------------------------      |
| `Nome`           | `string`    | **Obrigatório**. Informar nome do curso                   |

## Obter Curso

Ao obter curso
```http
    POST /api/usuarios/loginAction
```
| Parâmetro        | Tipo       | Descrição                                                 |
| :--------------- | :-------   | :---------------------------------------------------      |
| `nome`           | `string`   | **Obrigatório**. Informar o nome                          |

## Apagar Curso
 Para apagar curso 
 
```http
    POST /api/usuarios/loginAction
```
| Parâmetro        | Tipo       | Descrição                                                 |
| :--------------- | :-------   | :---------------------------------------------------      |
| `nome`           | `string`   | **Obrigatório**. Informar o nome                          |

</details>

<hr>

<details><summary><strong>Diciplinas</strong></summary>

## Obter Diciplinas

 Para obter diciplinas
 
```http
    POST /api/diciplinas/obterDiciplinas
```
**Sem parametros**

## Obter Diciplinas por {ID}

 Para obter Disciplina por id
 
```http
    POST /api/diciplinas/obterDiciplinas{id}
```
| Parâmetro        | Tipo       | Descrição                                                 |
| :--------------- | :-------   | :---------------------------------------------------      |
| `id`             | `integer`  | **Obrigatório**. Informar o id                            |

</details>

<hr>

<details><summary><strong>Questões</strong></summary>

## Obter Questões

```http
    POST /api/questoes/obterQuestoes
```
Sem parametros!

## Obter Questões{id}

```http
    POST /api/questoes/obterQuestoes{id}
```
| Parâmetro        | Tipo       | Descrição                                                 |
| :--------------- | :-------   | :---------------------------------------------------      |
| `id`             | `integer`  |  Informar o id                                            |

## Responder Questões

```http
    POST /api/questoes/responderQuestoes
```
Para responder a questão:

| Parâmetro         | Tipo       | Descrição                                                 |
| :---------------  | :-------   | :---------------------------------------------------      |
| `idQuestoe_Provas`| `integer`  |  Informar o id da questão                                 |
| `Resposta`        | `integer`  |  Informar o numero da questão que quer responder          |

## Cadastrar Questões

```http
    POST /api/questoes/cadastrarQuestoes
```

| Parâmetro         | Tipo       | Descrição                                                 |
| :---------------  | :-------   | :---------------------------------------------------      |
| `idQuestoe_Provas`| `integer`  | Id da questão alto encrementavel                          |
| `enunciado`       | `string`   | Informe o enunciado da questão                            |
| `op1`             | `string`   | Informe o texto para a auternativa 1                      |
| `op2`             | `string`   | Informe o texto para a auternativa 2                      |
| `op3`             | `string`   | Informe o texto para a auternativa 3                      |
| `op4`             | `string`   | Informe o texto para a auternativa 4                      |
| `op_correta`      | `integer`  | Informe o numero da auternativa correta                   |
| `dificuldade`     | `integer`  | Informe o grau de dificuldade da questão <br> **( facil, medio, dificil )**|  

## Deletar Questão

Para deletar uma questão

```http
    POST /api/questoes/apagarQuestoes{id}
```
| Parâmetro         | Tipo       | Descrição                                                 |
| :---------------  | :-------   | :---------------------------------------------------      |
| `idQuestoe_Provas`| `integer`  | Id da que deseja deletar                                  |

</details>

<hr>

<details><summary><strong>Turmas</strong></summary>

# Descrição de atividades - Reset de Senha

<details><summary>Solicitar nova senha - Perdi a senha</summary>

## Obter Turmas

```http
   POST /api/turmas/obterTurmas
```
Sem Parametros!

## Obter Turma{id}

```http
   POST /api/turmas/obterTurmas{id}
```
| Parâmetro         | Tipo       | Descrição                                           |
| :---------------  | :-------   | :---------------------------------------------------|
| `id_turma`        | `integer`  | Id da que deseja obter a turma                      |

## Criar turma
Para Criar uma turma

| Parâmetro         | Tipo       | Descrição                                           |
| :---------------  | :-------   | :---------------------------------------------------|
| `id_turma`        | `integer`  | id da turma auto encrementavel                      |
| `id_curso`        | `integer`  | id do curso que a turma vai ser vinculada           |
| `nome_turma`      | `string`   | Informe o nome da turma                             |

## Deletar turma
Para deletar turma 
```http
DELETE /api/turmas/{id}
```

| Parâmetro         | Tipo       | Descrição                                           |
| :---------------  | :-------   | :---------------------------------------------------|
| `id_turma`        | `integer`  | id da turma que deseja deletar                      |


</details> 

 <hr>
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


# Descrição de atividades - Login

<details><summary>Realizar login</summary>
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
               <tr>
                    <th style="width: 25%;">
                        Gerar TOKEN
                    </th>
                    <td>
                        O token de autorização tem validade de 1 hora.
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


