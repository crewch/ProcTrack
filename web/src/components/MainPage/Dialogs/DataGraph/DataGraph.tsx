import { useCallback } from 'react'
import ReactFlow, {
	Controls,
	Background,
	useNodesState,
	useEdgesState,
	addEdge,
	Edge,
	Connection,
} from 'reactflow'
import 'reactflow/dist/style.css'

const acceptanceColor = '#84DE02'
const rejectionColor = '#E32636'

const initialNodes = [
	{
		id: '1',
		position: { x: 200, y: 200 },
		data: { label: '1' },
		style: {
			background: acceptanceColor,
			border: 'none',
		},
	},
	{
		id: '2',
		position: { x: 0, y: 300 },
		data: { label: '2' },
		style: {
			background: acceptanceColor,
			border: 'none',
		},
	},
	{
		id: '3',
		position: { x: 400, y: 300 },
		data: { label: '3' },
		style: {
			background: rejectionColor,
			border: 'none',
		},
	},
]
const initialEdges = [
	{ id: 'e1-2', source: '1', target: '2' },
	{ id: 'e1-3', source: '1', target: '3', animated: true },
]

function DataGraph() {
	const [nodes, _setNodes, onNodesChange] = useNodesState(initialNodes)
	const [edges, setEdges, onEdgesChange] = useEdgesState(initialEdges)

	const onConnect = useCallback(
		(params: Edge | Connection) => setEdges(eds => addEdge(params, eds)),
		[setEdges]
	)

	return (
		<div
			style={{
				height: '100%',
				display: 'flex',
			}}
		>
			<ReactFlow
				nodes={nodes}
				edges={edges}
				onNodesChange={onNodesChange}
				onEdgesChange={onEdgesChange}
				onConnect={onConnect}
			>
				<Controls />
				<Background gap={12} size={1} />
			</ReactFlow>
		</div>
	)
}

export default DataGraph
