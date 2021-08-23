import React from 'react'

export default function Test({newUser}) {

    const { firstName, lastName, email} = newUser;

    return (
        <div>
            <span>eu sou~: {firstName}</span>
            <span> meu apelido é {lastName} </span> 
            <span>email: {email}</span>
            
        </div>
    )
}
