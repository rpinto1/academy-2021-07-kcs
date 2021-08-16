import React from 'react'

export default function NoNewsError() {
    return (
        <Message icon>
            <Icon name='circle notched' loading />
            <Message.Content>
                <Message.Header>Just one second</Message.Header>
                    We are fetching that content for you.
            </Message.Content>
        </Message>
    )
}
