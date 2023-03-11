import { Button } from '@mui/material'
import React, { useEffect, useState } from 'react'

export default function Home() {

  console.log('render')

  useEffect(() => {
    console.log('render effect')
  })

  const [counter, setCounter] = useState(0);
  return (

    <div>
      <h1>Home Page {counter}</h1>
      <Button onClick={() => setCounter((o) => o + 1)}>PLUS</Button>
    </div>
  )
}
