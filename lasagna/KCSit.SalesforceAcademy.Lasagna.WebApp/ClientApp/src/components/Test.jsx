import React from 'react'

export default function Test({newUser}) {

    const { firstName, lastName} = newUser;

    return (
        <div>
            <h1>eu sou{firstName}</h1>
        </div>
    )
}
