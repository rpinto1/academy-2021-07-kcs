import React from 'react'

export default function UserGreeting({user}) {

    const [firstName] = user;

    return (
            <section className="greeting">
                <article className="avatar">
                    <img src="../blank-avatar-sm.jpg" alt="" />
                </article>

                <article>
                    <h1>Hello, (firstName)!</h1>
                    <a href="http://localhost:3010/user/edit">Edit my profile</a>
                </article>
            </section>
    )
}
