import React, { useState } from 'react'
import { Button, List, ListContent, ListItem } from 'semantic-ui-react'

export const Company = () => {

    
    return (
        <List.Item className="listItems">        
            <List.Content floated='right'>
                <Button size='mini'>Add</Button>
            </List.Content>
            <List.Icon name='angle right' size='large' />
            <List.Content className='item'>   
                <List.Header>BAC</List.Header>
                <List.Description>Bank Of America</List.Description>
            </List.Content>
        </List.Item>
    )
}
